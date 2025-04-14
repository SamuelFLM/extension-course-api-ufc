import React from "react";
import { Routes, Route } from "react-router-dom";
import LoginComponent from "./login/LoginComponent";
import CategoryComponent from "./Category/CategoryComponent";

const AppRoutes: React.FC = () => {
  return (
    <Routes>
      <Route path="/" element={<LoginComponent />} />
      <Route path="/category" element={<CategoryComponent />} />
    </Routes>
  );
};

export default AppRoutes;
