const { Model } = require('objection');

//Defines a transactionc ategory
class TransactionCategory extends Model {

    //Table name
    static get tableName() {
        return 'transaction_categories';
    }

    //Composite ID
    static get idColumn() {
        return ['account_id', 'name'];
    }

    //Schema
    static get jsonSchema () {
        return {
            type: 'object',
            required: ['account_id', 'name'],
            properties: {
                account_id: {type: 'integer'},
                name: {type: 'string', minLength: 3, maxLength: 255}
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
                    from: 'transaction_categories.account',
                    to: 'accounts.id'
                }
            }
        }
    }

}

module.exports = TransactionCategory;
