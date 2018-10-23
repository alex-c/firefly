const express = require('express');
const router = express.Router();

//Load required models and error objects
const Account = require('../../models/Account.js');
const TransactionCategory = require('../../models/TransactionCategory.js');
const Transaction = require('../../models/Transaction.js');
const { ValidationError } = require('objection');

//Authorization middleware
let authorizeAdmin = require('./middleware/authorizeAdmin.js');

//GET /accounts -- Get a list of accounts.
router.get('/', async function(req, res, next) {

    try {
        const accounts = await Account.query();
        res.json(accounts);
    } catch (error) {
        next(error);
    }

});

//GET /accounts -- Get a specific account.
router.get('/:id', async function(req, res, next) {

    let id = req.params.id;

    if (isNaN(id)) {
        res.status(400).json({"message": "An account ID is a number."});
    } else {

        try {
            const account = await Account.query().where('id', id).first();
            if (account !== undefined) {
                res.json(account);
            } else {
                res.status(404).end();
            }
        } catch (error) {
            next(error);
        }

    }

});

//POST /accounts -- Create an account.
router.post('/', authorizeAdmin, async function(req, res, next) {

    if (req.body.name) {

        let account = {
            name: req.body.name,
            balance: req.body.balance || 0
        }

        try {
            account = await Account.query().insert(account);
            res.status(201).json(account);
        } catch (error) {
            if (error instanceof ValidationError) {
                res.status(400).json({"message": error.message});
            } else {
                next(error);
            }
        }

    } else {
        res.status(400).json({"message": "Missing information."});
    }

});

//DELETE /accounts/{id} -- Delete a specific account.
router.delete('/:id', authorizeAdmin, async function(req, res, next) {

    let id = req.params.id;

    if (isNaN(id)) {
        res.status(400).json({"message": "An account ID is a number."});
    } else {

        try {
            await Account.query().delete().where('id', id);
            res.end();
        } catch (error) {
            next(error);
        }

    }

});

//GET /accounts/{id}/categories -- Get a list of transaction categories available on this account.
router.get('/:id/categories', async function(req, res, next) {

    if (req.params.id) {

        let id = req.params.id;

        if (isNaN(id)) {
            res.status(400).json({"message": "A category ID is a number."});
        } else {

            try {
                const categories = await TransactionCategory.query().where('account_id', id);
                res.json(categories);
            } catch (error) {
                next(error);
            }

        }

    } else {
        res.status(400).json("Missing information!");
    }

});

//POST /accounts/{id}/categories -- Create a transaction category for this account.
router.post('/:id/categories', async function(req, res, next) {

    if (req.params.id && req.body.name) {

        let category = {
            account_id: req.params.id,
            name: req.body.name
        }

        if (isNaN(category.account_id)) {
            res.status(400).json({"message": "A category ID is a number."});
        } else {

            category.account_id = parseInt(category.account_id);

            try {
                const account = await Account.query().where('id', category.account_id).first();
                if (account !== undefined) {
                    //Store category
                    category = await TransactionCategory.query().insert(category);
                    //Return new category
                    res.status(201).json(category);
                } else {
                    res.status(404).end();
                }
            } catch (error) {
                next(error);
            }

        }

    } else {
        res.status(400).json("Missing information!");
    }

});

//GET /accounts/{id}/transactions -- Get a list of transactions for this account.
router.get('/:id/transactions', async function(req, res, next) {

    if (req.params.id) {

        let id = req.params.id;
        let category = req.query.category || null;

        if (isNaN(id)) {
            res.status(400).json({"message": "An account ID is a number."});
        } else {

            try {
                let transactions = null;
                if (category !== null) {
                    transactions = await Transaction.query().where('account_id', id).andWhere('category_id', category).orderBy('created_at', 'desc');
                } else {
                    transactions = await Transaction.query().where('account_id', id).orderBy('created_at', 'desc');
                }
                res.json(transactions);
            } catch (error) {
                next(error);
            }

        }

    } else {
        res.status(400).json("Missing information!");
    }

});

//POST /accounts/{id}/transactions -- Book a transaction to this account.
router.post('/:id/transactions', async function(req, res, next) {

    if (req.params.id && req.body.value) {

        let transaction = {
            account_id: req.params.id,
            value: req.body.value,
            created_at: req.body.createdAt || new Date().toISOString(),
            category_id: req.body.category || null
        }

        if (isNaN(transaction.account_id)) {
            res.status(400).json({"message": "An account ID is a number."});
        } else {

            try {
                const account = await Account.query().where('id', transaction.account_id).first();
                if (account !== undefined) {
                    //Store transaction
                    transaction = await Transaction.query().insert(transaction);
                    //Update account balance
                    let newBalance = account.balance + transaction.value;
                    await Account.query().patchAndFetchById(transaction.account_id, {balance: newBalance});
                    //Return new transaction
                    res.status(201).json(transaction);
                } else {
                    res.status(404).end();
                }
            } catch (error) {
                next(error);
            }

        }

    } else {
        res.status(400).json("Missing information!");
    }

});

module.exports = router;
