const express = require('express');
const router = express.Router();

//Load required models
const Account = require('../../models/Account.js');

//GET /api/accounts -- Get a list of accounts.
router.get('/', async function(req, res, next) {

    try {
        const accounts = await Account.query();
        res.json(accounts);
    } catch (error) {
        next(error);
    }

});

//POST /api/accounts -- Create an account.
router.post('/', async function(req, res, next) {

    if (req.body.name) {

        let balance = req.body.balance || 0;

        try {
            await Account.query().insert({name: req.body.name, balance});
            res.status(201).end();
        } catch (error) {
            next(error);
        }

    } else {
        res.status(400).json("Missing information!");
    }

});


module.exports = router;
