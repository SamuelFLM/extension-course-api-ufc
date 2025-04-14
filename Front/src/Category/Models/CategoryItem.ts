import ProductItem from "./ProductItem";

interface CategoryItem {
  id: number;
  name: string;
  products: ProductItem[];
}

export default CategoryItem;