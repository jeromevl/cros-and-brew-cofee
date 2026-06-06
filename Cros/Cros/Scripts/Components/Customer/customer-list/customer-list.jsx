function CustomerList() {
    const [customers, setCustomers] = React.useState({ hasLoaded: false, items: [] });
    const [confirmDelete, setConfirmDelete] = React.useState(null);

    React.useEffect(loadCustomers, []);

    function loadCustomers() {
        fetch(`/api/customers?pagenumber=${1}&pagesize=${999}&searchtext=${''}`, {
            method: "GET",
        })
            .then(response => response.json())
            .then(response => {
                response.items.forEach(function (cust) {
                    cust.name = `${cust.lastName}, ${cust.firstName}`
                });

                setCustomers({ hasLoaded: true, items: response.items });
            })
            .catch(error => {
                console.log(error)
            });
    }

    function addCustomer() {
        window.location.href = "/customers/new";
    }

    function editCustomer(id) {
        window.location.href = `/customers/edit/${id}`;
    }

    function confirmDeleteCustomer(id) {
        setConfirmDelete({
            message: "Are you sure you want to delete the selected customer?",
            onConfirm: () => deleteCustomer(id)
        });
    }

    function deleteCustomer(id) {
        fetch(`/api/customers/${id}`, {
            method: "DELETE",
            headers: {
                "Content-type": "application/json; charset=UTF-8",
            }
        })
            .then(response => {
                if (response.ok)
                    setCustomers({ ...customers, items: customers.items.filter(c => c.id !== id) });

                setConfirmDelete(null);
            })
            .catch(error => {
                console.log(error);
            })
    }

    return (
        <div className="customer-container">
            <div className="list-header">
                <h2>Customer List</h2>
                <button className="add-btn" onClick={addCustomer}>+ Add New</button>
            </div>
            {
                customers.hasLoaded ?
                    <React.Fragment>
                        {
                            customers.items.length === 0 ?
                                <div className="empty-state">
                                    <p>No customers yet!</p>
                                    <button className="add-btn" onClick={addCustomer}>Add Customer</button>
                                </div>
                                :
                                <ul className="customer-list">
                                    {
                                        customers.items.map(cust => (
                                            <li key={cust.id} className="customer-item">
                                                <div className="customer-info">
                                                    <strong>{cust.name}</strong>
                                                    <div>{cust.emailAddress || "< no email address specified >"}</div>
                                                </div>
                                                <div className="customer-actions">
                                                    <button className="edit-btn" onClick={() => editCustomer(cust.id)}>Edit</button>
                                                    <button className="delete-btn" onClick={() => confirmDeleteCustomer(cust.id)}>Delete</button>
                                                </div>
                                            </li>
                                        ))
                                    }
                                </ul>
                        }
                    </React.Fragment>
                    :
                    <Loader />
            }
            {
                confirmDelete && (
                    <ConfirmDialog message={confirmDelete.message}
                        onConfirm={confirmDelete.onConfirm}
                        onCancel={() => setConfirmDelete(null)}
                    />
                )
            }
        </div>
    );
}

const root = ReactDOM.createRoot(document.getElementById('root'));
root.render(<CustomerList />);