import { Button, Modal } from "react-bootstrap";
import ToastComponent from "../../login/components/ToastComponent";
import { useState } from "react";
import { putCategory } from "../../services/authService";
import CategoryItem from "../Models/CategoryItem";

type UpdateCategoryModalProps = {
  showModal: boolean;
  handleClose: () => void;
  idCategory: number;
  setCategories: React.Dispatch<React.SetStateAction<CategoryItem[]>>;
  refreshCategories: () => Promise<void>;
};

const UpdateCategoryModalComponent: React.FC<UpdateCategoryModalProps> = (
  props
) => {
  const [categoryName, setCategoryName] = useState("");
  const [error, setError] = useState("");
  const [response, setResponse] = useState(false);

  const handleUpdateCategory = async (e: React.FormEvent) => {
    e.preventDefault();

    if (!categoryName.trim()) {
      setError("Category name is required.");
      return;
    }
    try {
      const response = await putCategory(props.idCategory, categoryName);
      if (response && response.data && Array.isArray(response.data)) {
        props.setCategories((prev) =>
          prev.map((category) =>
            category.id === props.idCategory
              ? { ...category, name: categoryName }
              : category
          )
        );
        setResponse(true);
      } else {
        setResponse(false);
      }
    } catch (error) {
      console.log(error);
    }
  };

  return (
    <>
      <Modal show={props.showModal} onHide={props.handleClose}>
        <Modal.Header closeButton>
          <Modal.Title>Update Category</Modal.Title>
        </Modal.Header>

        <Modal.Body>
          <div className="mb-3">
            <label htmlFor="categoryName" className="form-label">
              Category
            </label>
            <input
              type="text"
              className="form-control"
              id="categoryName"
              value={categoryName}
              onChange={(e) => setCategoryName(e.target.value.trim())}
            />
          </div>
          <Button
            className="btn btn-success w-100 mb-2"
            onClick={handleUpdateCategory}
          >
            Update
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

export default UpdateCategoryModalComponent;
