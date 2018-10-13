const express = require('express');
const router = express.Router();

//Load required models and error objects
const Account = require('../../models/Account.js');
const { ValidationError } = require('objection');

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
            if (account === undefined) {
                res.status(404).end();
            } else {
                res.json(account);
            }
        } catch (error) {
            next(error);
        }

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


module.exports = router;
