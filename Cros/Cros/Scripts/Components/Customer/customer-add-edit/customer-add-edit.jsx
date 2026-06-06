function CustomerAddEdit() {
    const customerId = window.location.pathname.split("/").pop() || null;

    const [title, setTitle] = React.useState("");
    const [errors, setErrors] = React.useState({});

    const [customer, setCustomer] = React.useState({
        id: customerId || 0,
        customerNo: "",
        firstName: "",
        middleName: "",
        lastName: "",
        nameSuffix: "",
        emailAddress: "",
        mobilePhoneNo: ""
    });

    const [savedSignature, setSavedSignature] = React.useState(null);

    const [hasLoaded, setHasLoaded] = React.useState(false);
    const [isSaving, setIsSaving] = React.useState(false);

    React.useEffect(() => {
        if (hasLoaded && canvasRef.current) {
            signaturePadRef.current = new SignaturePad(canvasRef.current);
            if (savedSignature)
                signaturePadRef.current.fromDataURL(savedSignature);
        }
    }, [hasLoaded]);

    React.useEffect(loadCustomer, []);

    const canvasRef = React.useRef(null);
    const signaturePadRef = React.useRef(null);

    function loadCustomer() {
        if (customer.id > 0) {
            fetch(`/api/customers/${customerId}`, {
                method: "GET"
            })
                .then(response => response.json())
                .then(response => {
                    setCustomer(response);
                    setSavedSignature(response.signature);

                    setTitle(`Edit Customer - ${response.lastName}, ${response.firstName}`);
                    setHasLoaded(true);
                })
                .catch(error => console.log(error));
        }
        else {
            setTitle("Add Customer");
            setHasLoaded(true);
        }
    }

    function handleChange(e) {
        setCustomer({ ...customer, [e.target.name]: e.target.value });
    }

    function save() {
        if (!validate())
            return;

        const formData = new FormData();
        formData.append("id", customer.id);
        formData.append("customerNo", customer.customerNo);
        formData.append("firstName", customer.firstName ?? "");
        formData.append("middleName", customer.middleName ?? "");
        formData.append("lastName", customer.lastName ?? "");
        formData.append("nameSuffix", customer.nameSuffix || "");
        formData.append("emailAddress", customer.emailAddress || "");
        formData.append("mobilePhoneNo", customer.mobilePhoneNo ?? "");

        const signatureData = signaturePadRef.current && !signaturePadRef.current.isEmpty()
            ? signaturePadRef.current.toDataURL()
            : "";

        formData.append("signature", signatureData);

        let method = "POST";
        if (customer.id)
            method = "PUT";

        fetch("/api/customers", {
            method: method,
            body: formData
        })
            .then(response => {
                if (response.ok)
                    window.location.href = "/customers";
            })
            .catch(error => {
                console.log(error);
            });
    }

    function validate() {
        const err = {};
        if (!customer.customerNo.trim())
            err.customerNo = "Customer No. is required";

        if (!customer.firstName.trim())
            err.firstName = "First Name is required";

        if (!customer.lastName.trim())
            err.lastName = "Last Name is required";

        if (customer.emailAddress && customer.emailAddress.trim() && !/^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(customer.emailAddress.trim()))
            err.emailAddress = "Enter a valid email address";

        setErrors(err);

        return Object.keys(err).length === 0;
    }

    function cancel() {
        window.location.href = "/customers";
    }

    function clearSignature() {
        signaturePadRef.current.clear();
    }

    function handleChange(e) {
        setCustomer({ ...customer, [e.target.name]: e.target.value });
        if (errors[e.target.name])
            setErrors({ ...errors, [e.target.name]: undefined });
    }

    if (!hasLoaded) return <Loader />;

    return (
        <div className="form-container">
            <div className="form-header">
                <h2>{title}</h2>
            </div>

            <div className="form-card">
                <div className="form-section-title">Basic Information</div>

                <div className="form-row">
                    <div className="form-group">
                        <label>Customer No. <span className="required">*</span></label>
                        <input type="text" className={errors.customerNo ? "input-error" : ""} name="customerNo" value={customer.customerNo} onChange={handleChange} placeholder="e.g. CUST-0001" autoFocus />
                        {errors.customerNo && <span className="error-msg">{errors.customerNo}</span>}
                    </div>
                </div>

                <div className="form-row col-3">
                    <div className="form-group">
                        <label>First Name <span className="required">*</span></label>
                        <input type="text" className={errors.firstName ? "input-error" : ""} name="firstName" value={customer.firstName} onChange={handleChange} placeholder="First name" />
                        {errors.firstName && <span className="error-msg">{errors.firstName}</span>}
                    </div>
                    <div className="form-group">
                        <label>Middle Name</label>
                        <input type="text" name="middleName" value={customer.middleName} onChange={handleChange} placeholder="Middle name" />
                    </div>
                    <div className="form-group">
                        <label>Last Name <span className="required">*</span></label>
                        <input type="text" className={errors.lastName ? "input-error" : ""} name="lastName" value={customer.lastName} onChange={handleChange} placeholder="Last name" />
                        {errors.lastName && <span className="error-msg">{errors.lastName}</span>}
                    </div>
                </div>

                <div className="form-row">
                    <div className="form-group form-group-sm">
                        <label>Suffix</label>
                        <input type="text" name="nameSuffix" value={customer.nameSuffix} onChange={handleChange} placeholder="e.g. Jr., Sr." />
                    </div>
                </div>

                <div className="form-divider"></div>
                <div className="form-section-title">Contact Information</div>

                <div className="form-row col-2">
                    <div className="form-group">
                        <label>Email Address</label>
                        <input type="email" className={errors.emailAddress ? "input-error" : ""} name="emailAddress" value={customer.emailAddress} onChange={handleChange} placeholder="email@example.com" />
                        {errors.emailAddress && <span className="error-msg">{errors.emailAddress}</span>}
                    </div>
                    <div className="form-group">
                        <label>Mobile Phone No</label>
                        <input type="tel" name="mobilePhoneNo" value={customer.mobilePhoneNo} onChange={handleChange} placeholder="+63 912 345 6789" />
                    </div>
                </div>

                <div className="form-divider"></div>
                <div>
                    <div className="form-section-title">Signature</div>
                    <div className="signature-wrapper">
                        <canvas ref={canvasRef} width={400} height={200} className="signature-canvas"></canvas>
                        <button type="button" className="clear-signature-btn" onClick={clearSignature}>Clear</button>
                    </div>
                </div>
            </div>

            <div className="form-actions">
                <button className="cancel-btn" onClick={cancel}>Cancel</button>
                <button className="save-btn" onClick={save} disabled={isSaving}>
                    {isSaving ? "Saving..." : "Save"}
                </button>
            </div>
        </div>
    );
}

const root = ReactDOM.createRoot(document.getElementById('root'));
root.render(<CustomerAddEdit />);