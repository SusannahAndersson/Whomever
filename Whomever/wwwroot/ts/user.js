var User = /** @class */ (function () {
    function User(firstName, lastName) {
        this.firstName = firstName;
        this.lastName = lastName;
    }
    User.prototype.showName = function () {
        alert("".concat(this.firstName, " ").concat(this.lastName));
    };
    return User;
}());
//# sourceMappingURL=user.js.map