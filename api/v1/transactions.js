const express = require('express');
const router = express.Router();

//Load required models
const Account = require('../../models/Account.js');
const Transaction = require('../../models/Transaction.js');

//GET /api/transactions -- Get a list of transactions.
router.get('/', async function(req, res, next) {

    if (req.body.account) {

        try {
            const transactions = await Transaction.query().where('account', req.body.account);
            res.json(transactions);
        } catch (error) {
            next(error);
        }

    } else {
        res.status(400).json("Missing information!");
    }

});

//POST /api/transactions -- Create a transaction.
router.post('/', async function(req, res, next) {

    if (req.body.account && req.body.value) {

        let accountId = req.body.account;
        let value = req.body.value;

        try {
            const account = await Account.query().where('id', accountId).first();
            if (account !== undefined) {
                let newBalance = account.balance + value;
                const transaction = await Transaction.query().insert({account: accountId, value});
                await Account.query().patchAndFetchById(accountId, {balance: newBalance});
                res.status(201).json({transaction: transaction.id});
            } else {
                res.status(404).end();
            }
        } catch (error) {
            next(error);
        }

    } else {
        res.status(400).json("Missing information!");
    }

});


module.exports = router;
