import { Modal } from "react-bootstrap";

type ProductModalProps = {
  showModal: boolean;
  handleClose: () => void;
};

const ProductModalComponent: React.FC<ProductModalProps> = (props) => {
  return (
    <>
      <Modal show={props.showModal} onHide={props.handleClose}>
        <Modal.Header closeButton>
          <Modal.Title>Products</Modal.Title>
        </Modal.Header>
        <Modal.Body>
          <h1>Olá</h1>
        </Modal.Body>
      </Modal>
    </>
  );
};

export default ProductModalComponent;
