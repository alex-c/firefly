const express = require('express');
const router = express.Router();

//Load required models
const Transaction = require('../../models/Transaction.js');

//GET /api/transactions -- Get a list of transactions.
router.get('/', async function(req, res, next) {

    if (req.query.account) {
        try {
            const transactions = await Transaction.query().where('account', req.query.account);
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

        let account = req.body.account;
        let value = req.body.value;

        try {
            const transaction = await Transaction.query().insert({account, value});
            res.status(201).json({transaction: transaction.id});
        } catch (error) {
            next(error);
        }

    } else {
        res.status(400).json("Missing information!");
    }

});


module.exports = router;
