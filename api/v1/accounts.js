const express = require('express');
const router = express.Router();

//Load required models and error objects
const Account = require('../../models/Account.js');
const Transaction = require('../../models/Transaction.js');
const { ValidationError } = require('objection');

//Authorization middleware
let authorizeAdmin = require('./middleware/authorizeAdmin.js');

//GET /api/accounts -- Get a list of accounts.
router.get('/', async function(req, res, next) {

    try {
        const accounts = await Account.query();
        res.json(accounts);
    } catch (error) {
        next(error);
    }

});

//GET /api/accounts -- Get a specific account.
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

//POST /api/accounts -- Create an account.
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

//DELETE /api/accounts/{id} -- Delete a specific account.
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

//GET /api/accounts/{id}/transactions -- Get a list of transactions.
router.get('/:id/transactions', async function(req, res, next) {

    if (req.params.id) {

        let id = req.params.id;

        if (isNaN(id)) {
            res.status(400).json({"message": "An account ID is a number."});
        } else {

            try {
                const transactions = await Transaction.query().where('account', id).orderBy('created_at', 'desc');
                res.json(transactions);
            } catch (error) {
                next(error);
            }

        }

    } else {
        res.status(400).json("Missing information!");
    }

});

//POST /api/accounts/{id}/transactions -- Book a transaction.
router.post('/:id/transactions', async function(req, res, next) {

    if (req.params.id && req.body.value) {

        let transaction = {
            account: req.params.id,
            value: req.body.value,
            created_at: req.body.createdAt || new Date().toISOString()
        }

        if (isNaN(transaction.account)) {
            res.status(400).json({"message": "An account ID is a number."});
        } else {

            try {
                const account = await Account.query().where('id', transaction.account).first();
                if (account !== undefined) {
                    //Store transaction
                    transaction = await Transaction.query().insert(transaction);
                    //Update account balance
                    let newBalance = account.balance + transaction.value;
                    await Account.query().patchAndFetchById(transaction.account, {balance: newBalance});
                    //Return new transaction ID
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
