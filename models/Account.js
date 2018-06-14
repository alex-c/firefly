const { Model } = require('objection');

//Defines a Firefly user
class Account extends Model {

    //Table name
    static get tableName() {
        return 'accounts';
    }

    //ID column
    static get idColumn() {
        return 'id';
    }

    //Schema
    static get jsonSchema () {
        return {
            type: 'object',
            required: ['name'],
            properties: {
                id: {type: 'integer'},
                name: {type: 'string', minLength: 3, maxLength: 255},
                balance: {type: 'number'}
            }
        };
    }
}

module.exports = Account;
