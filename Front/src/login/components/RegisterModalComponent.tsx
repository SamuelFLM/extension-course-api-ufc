import { useState } from "react";
import { Button, Modal } from "react-bootstrap";
import { register } from "../../services/authService";
import ToastComponent from "./ToastComponent";

type RegisterModalProps = {
  showModal: boolean;
  handleClose: () => void;
};

const RegisterModalComponent: React.FC<RegisterModalProps> = (props) => {
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");
  const [error, setError] = useState("");
  const [response, setResponse] = useState(false);

  const handleRegister = async (e: React.FormEvent) => {
    e.preventDefault();
    setError("");

    try {
      const data = await register(username, password);

      if (data) {
        setResponse(true);
      } else setResponse(false);
      // Redirect or update application state as needed
    } catch (error) {
      setError("Register failed. Please check your credentials." + error);
    }
  };

  return (
    <>
      <Modal show={props.showModal} onHide={props.handleClose}>
        <Modal.Header closeButton>
          <Modal.Title>Sign Up</Modal.Title>
        </Modal.Header>
        <Modal.Body>
          <div className="mb-3">
            <label htmlFor="username" className="form-label">
              Username
            </label>
            <input
              type="text"
              className="form-control"
              id="username"
              value={username}
              onChange={(e) => setUsername(e.target.value.trim())}
            />
          </div>
          <div className="mb-3">
            <label htmlFor="password" className="form-label">
              Password
            </label>
            <input
              type="password"
              className="form-control"
              id="password"
              value={password}
              onChange={(e) => setPassword(e.target.value.trim())}
            />
          </div>
          <Button
            className="btn btn-primary w-100 mb-2"
            onClick={handleRegister}
          >
            Register
          </Button>
        </Modal.Body>
        {error.length > 0 && <ToastComponent message={error} color="red" />}
        {response && (
          <ToastComponent
            message="User registered successfully"
            color="green"
          />
        )}
      </Modal>
    </>
  );
};

export default RegisterModalComponent;
