import React, { useState } from "react";
import "bootstrap/dist/css/bootstrap.min.css";
import RegisterModalComponent from "./components/RegisterModalComponent";
import { login } from "../services/authService";
import ToastComponent from "./components/ToastComponent";
import { useNavigate } from "react-router-dom";

const LoginComponent: React.FC = () => {
  const navigate = useNavigate();
  const [showModal, setShowModal] = useState(false);
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");
  const [error, setError] = useState("");
  const [response, setResponse] = useState(false);

  const handleShow = () => {
    setShowModal(true);
  };

  const handleLogin = async (e: React.FormEvent) => {
    e.preventDefault();
    setError("");

    try {
      const data: boolean = await login(username, password);
      if (data) {
        setResponse(true);
        navigate("/test"); // Redirect to home page on successful login
      } else {
        setError("Login failed. Please check your credentials.");
        setResponse(false);
      }
      // Redirect or update application state as needed
    } catch (error) {
      setError("Login failed. Please check your credentials." + error);
    }
  };
  const handleClose = () => setShowModal(false);

  return (
    <div className="d-flex justify-content-center align-items-center vh-100">
      <form
        className="card p-4 shadow"
        style={{ maxWidth: "400px", width: "100%" }}
      >
        <div className="text-center mb-3">
          <h1 className="h3">Login</h1>
        </div>
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
        <button className="btn btn-primary w-100 mb-2" onClick={handleLogin}>
          Login
        </button>
        <div className="text-center">
          <small>Or Sign Up Using</small>
        </div>
        <div className="text-center mt-3">
          <button type="button" className="btn btn-link" onClick={handleShow}>
            <a>Sign Up</a>
          </button>
        </div>
      </form>

      <RegisterModalComponent showModal={showModal} handleClose={handleClose} />

      {error.length > 0 && <ToastComponent message={error} color="red" />}
      {response && (
        <ToastComponent message="Login efetuado com sucesso." color="green" />
      )}
    </div>
  );
};

export default LoginComponent;
