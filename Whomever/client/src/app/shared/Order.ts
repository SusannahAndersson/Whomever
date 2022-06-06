export class OrderItem {
  id: number;
  quantity: number;
  unitPrice: number;
  productId: number;
  productCategory: string;
  productTitle: string;
  productSize: string;
  productBrand: string;
  productProductId: string;
}

export class Order {
  orderId: number;
  orderDate: Date = new Date;
  orderNumber: string;
  //exp orderitem array
  items: OrderItem[] = new Array<OrderItem>();
  //calculates total value from added items to cart
  get total(): number {
    const calc = this.items.reduce(
      (total, value) => {
        return total + (value.unitPrice * value.quantity);
      }, 0);
    return calc;
}
}
