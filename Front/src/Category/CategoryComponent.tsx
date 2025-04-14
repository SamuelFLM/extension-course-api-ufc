import React, { useEffect, useState } from "react";
import axios from "axios";
import {
  deleteCategory,
  getCategory,
  postCategory,
} from "../services/authService";
import ProductModalComponent from "./components/ProductComponent";
import UpdateCategoryModalComponent from "./components/UpdateCategoryModalComponent";
import PageEnum from "./Enum/PageEnum";
import CategoryItem from "./Models/CategoryItem";

const CategoryComponent: React.FC = () => {
  const [categories, setCategories] = useState<CategoryItem[]>([]);
  const [editingId, setEditingId] = useState<number | null>(null);
  const [newCategoryName, setNewCategoryName] = useState<string>("");
  const [modalAddCategory, setModalAddCategory] = useState(false);
  const [modalProductList, setModalProductList] = useState(false);
  const [modalEditCategory, setModalEditCategory] = useState(false);

  const fetchCategories = async () => {
    try {
      const response = await getCategory();
      if (Array.isArray(response.data)) {
        setCategories(response.data);
      } else {
        console.error("Invalid data format:", response.data);
      }
    } catch (error) {
      console.error("Error fetching categories:", error);
    }
  };

  useEffect(() => {
    fetchCategories();
  }, []);

  const handleCloseProductListShow = () => {
    setModalProductList(false);
    fetchCategories(); // Atualiza o estado ao fechar o modal
  };

  const handleCloseModalEditCategory = () => {
    setModalEditCategory(false);
    setEditingId(null);
    fetchCategories(); // Atualiza o estado ao fechar o modal
  };

  const handleShow = (active: number) => {
    switch (active) {
      case 1:
        setModalAddCategory(true);
        break;
      case 2:
        setModalProductList(true);
        break;
      case 3:
        setModalEditCategory(true);
        break;
    }
  };

  const handleAddCategory = async () => {
    if (!newCategoryName.trim()) return;

    try {
      const response = await postCategory(newCategoryName);
      setCategories((prev) => [...prev, response.data]);
      setNewCategoryName("");
      fetchCategories(); // Atualiza o estado após adicionar uma categoria
    } catch (error) {
      console.error("Error adding category:", error);
    }
  };

  const handleEdit = (id: number) => {
    setEditingId(id);
    handleShow(PageEnum.EDIT_CATEGORY);
  };

  const handleDelete = async (id: number) => {
    const response = await deleteCategory(id);
    try {
      if (response) {
        setCategories((prev) => prev.filter((category) => category.id !== id));
      } else {
        console.error("Error deleting category:", response);
      }
    } catch (error) {
      console.error("Error deleting category:", error);
    } finally {
      fetchCategories(); // Atualiza o estado após deletar uma categoria
    }
  };

  return (
    <div
      style={{
        fontFamily: "Arial, sans-serif",
        padding: "20px",
        backgroundColor: "#F5F5F5",
        minHeight: "100vh",
      }}
    >
      <h1
        style={{ color: "#005AA5", textAlign: "center", marginBottom: "20px" }}
      >
        Categories
      </h1>
      <div
        style={{
          display: "flex",
          justifyContent: "right",
          marginBottom: "20px",
        }}
      >
        <button
          onClick={handleAddCategory}
          style={{
            backgroundColor: "#005AA5",
            color: "#FFFFFF",
            border: "none",
            borderRadius: "5px",
            padding: "10px 40px",
            cursor: "pointer",
            display: "flex",
            alignItems: "center",
            gap: "10px",
          }}
        >
          <span style={{ fontSize: "20px", fontWeight: "bold" }}>+</span>
          Add Category
        </button>
      </div>
      <table
        style={{
          width: "100%",
          borderCollapse: "collapse",
          backgroundColor: "#FFFFFF",
          boxShadow: "0 4px 8px rgba(0, 0, 0, 0.1)",
        }}
      >
        <thead>
          <tr style={{ backgroundColor: "#005AA5", color: "#FFFFFF" }}>
            <th style={{ padding: "10px", textAlign: "left" }}>Name</th>
            <th style={{ padding: "10px", textAlign: "left" }}>Actions</th>
          </tr>
        </thead>
        <tbody>
          {categories.map((category) => (
            <tr
              key={category.id}
              style={{
                borderBottom: "1px solid #E0E0E0",
                backgroundColor: category.id % 2 === 0 ? "#F9F9F9" : "#FFFFFF",
              }}
            >
              <td style={{ padding: "10px" }}>{category.name}</td>
              <td style={{ padding: "10px", gap: "50px" }}>
                <button
                  onClick={() => handleShow(PageEnum.PRODUCT_LIST)}
                  style={{
                    backgroundColor: "#005AA5",
                    color: "#FFFFFF",
                    border: "none",
                    borderRadius: "5px",
                    padding: "5px 30px",
                    marginRight: "5px",
                    cursor: "pointer",
                  }}
                >
                  Products
                </button>

                <button
                  onClick={() => handleEdit(category.id)}
                  style={{
                    backgroundColor: "#FFC107",
                    color: "#FFFFFF",
                    border: "none",
                    borderRadius: "5px",
                    padding: "5px 30px",
                    marginRight: "5px",
                    cursor: "pointer",
                  }}
                >
                  Edit
                </button>

                <button
                  onClick={() => handleDelete(category.id)}
                  style={{
                    backgroundColor: "#DC3545",
                    color: "#FFFFFF",
                    border: "none",
                    borderRadius: "5px",
                    padding: "5px 30px",
                    marginRight: "5px",
                    cursor: "pointer",
                  }}
                >
                  Delete
                </button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
      {
        <ProductModalComponent
          showModal={modalProductList}
          handleClose={handleCloseProductListShow}
        />
      }

      {
        <UpdateCategoryModalComponent
          showModal={modalEditCategory}
          handleClose={handleCloseModalEditCategory}
          idCategory={editingId ? editingId : 0}
          setCategories={setCategories}
          refreshCategories={fetchCategories} // Passa a função para o modal
        />
      }
    </div>
  );
};

export default CategoryComponent;
