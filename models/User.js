const { Model } = require('objection');
const password = require('objection-password')();

//Defines a Firefly user
class User extends password(Model) {

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
                password: {type: 'string', minLength: 8, maxLength: 255},
                is_admin: {type: 'boolean'}
            }
        };
    }

    //Relations
    static get relationMappings() {
        const Account = require('./Account.js');
        return {
            accounts: {
                relation: Model.ManyToManyRelation,
                modelClass: Account,
                join: {
                    from: 'users.id',
                    through: {
                        from: 'account_access.user_id',
                        to: 'account_access.account_id'
                    },
                    to: 'accounts.id'
                }
            }
        }
    }
}

module.exports = User;
