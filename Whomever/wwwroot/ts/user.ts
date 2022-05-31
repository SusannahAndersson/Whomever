class User {
    constructor(private firstName: string, private lastName: string) {
    }

    showName() {
        alert(`${this.firstName} ${this.lastName}`);
    }
}