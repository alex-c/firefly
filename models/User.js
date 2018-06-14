const { Model } = require('objection');

//Defines a Firefly user
class User extends Model {

    //Table name
    static get tableName() {
        return 'users';
    }

    //ID column
    static get idColumn() {
        return 'id';
    }

    //Schema
    static get jsonSchema () {
        return {
            type: 'object',
            required: ['name', 'password'],
            properties: {
                id: {type: 'integer'},
                name: {type: 'string', minLength: 3, maxLength: 255},
                password: {type: 'string', minLength: 8, maxLength: 255}
            }
        };
    }
}

module.exports = User;
