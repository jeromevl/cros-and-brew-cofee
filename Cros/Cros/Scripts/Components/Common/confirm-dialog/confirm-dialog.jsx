function ConfirmDialog({ message, onConfirm, onCancel }) {
    return (
        <div className="confirm-overlay">
            <div className="confirm-dialog">
                <p>{message}</p>
                <div className="confirm-actions">
                    <button className="cancel-btn" onClick={onCancel}>Cancel</button>
                    <button className="danger-btn" onClick={onConfirm}>Confirm</button>
                </div>
            </div>
        </div>
    );
}