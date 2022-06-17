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
  //orderId: "";
  orderId: number;
  orderDate: Date = new Date;
  //need to create random string for ordernumber
  orderNumber: string = (0 | Math.random() * 9e6).toString(36);
  ////exp orderitem array
  items: OrderItem[] = new Array<OrderItem>();
  //items: OrderItem[] = [];

  ////calculates total value from added items to cart
  get total(): number {
    const calc = this.items.reduce(
      (total, value) => {
        return total + (value.unitPrice * value.quantity);
      }, 0);
    return calc;
  }
}
