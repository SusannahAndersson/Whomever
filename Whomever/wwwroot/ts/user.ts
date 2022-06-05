class AppUser {
    constructor(private firstName: string, private lastName: string) {
    }

    showName() {
        alert(`${this.firstName} ${this.lastName}`);
    }
}