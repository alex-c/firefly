const { Model } = require('objection');
const guid = require('objection-guid')();

//Defines a Firefly user
class Transaction extends guid(Model) {

    //Table name
    static get tableName() {
        return 'transactions';
    }

    //ID column
    static get idColumn() {
        return 'id';
    }

    //Schema
    static get jsonSchema() {
        return {
            type: 'object',
            required: ['value'],
            properties: {
                id: {type: 'guid'},
                value: {type: 'real'},
                created_at: {type: 'date-time'}
            }
        };
    }

    //Relations
    static get relationMappings() {
        const Account = require('./Account.js');
        return {
            account: {
                relation: Model.BelongsToOneRelation,
                modelClass: Account,
                join: {
                    from: 'transactions.account_id',
                    to: 'accounts.id'
                }
            }
        }
    }

}

module.exports = Transaction;
