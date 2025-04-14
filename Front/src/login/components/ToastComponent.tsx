import React, { useEffect, useState } from "react";
import "bootstrap/dist/css/bootstrap.min.css";
import { Button } from "react-bootstrap";
import "../style/toast.css";

interface ToastProps {
  message: string;
  color: string;
}

const ToastComponent: React.FC<ToastProps> = ({ message, color }) => {
  const [visible, setVisible] = useState(true);

  useEffect(() => {
    const timer = setTimeout(() => {
      setVisible(false);
    }, 5000);

    return () => clearTimeout(timer);
  }, []);

  if (!visible) return null;

  return (
    <div
      className="toast show position-fixed top-0 end-0 m-3"
      role="alert"
      aria-live="assertive"
      aria-atomic="true"
      style={{ backgroundColor: color, color: "#fff" }}
    >
      <div className="toast-header">
        <strong className="me-auto">Notification</strong>
        <Button
          type="button"
          className="btn-close btn-close-black"
          data-bs-dismiss="toast"
          aria-label="Close"
          onClick={() => setVisible(false)}
        ></Button>
      </div>
      <div className="toast-body">{message}</div>
    </div>
  );
};

export default ToastComponent;
