const express = require('express');
const router = express.Router();

//Load required models and error objects
const User = require('../../models/User.js');
const { ValidationError } = require('objection');

//GET /api/users -- Get a list of users.
router.get('/', async function(req, res, next) {

    try {
        const users = await User.query();
        res.json(users);
    } catch (error) {
        next(error);
    }

});

//GET /api/users/{id} -- Get a specific user.
router.get('/:id', async function(req, res, next) {

    let id = req.params.id;

    if (isNaN(id)) {
        res.status(400).json({"message": "A user ID is a number."});
    } else {

        try {
            const user = await User.query().where('id', id).first();
            if (user === undefined) {
                res.status(404).end();
            } else {
                res.json(user);
            }
        } catch (error) {
            next(error);
        }

    }

});

//POST /api/users -- Create a user.
router.post('/', async function(req, res, next) {

    if (req.body.name && req.body.password) {

        let user = {
            name: req.body.name,
            password: req.body.password,
            is_admin: req.body.isAdmin || false
        }

        try {
            await User.query().insert(user);
            res.status(201).end();
        } catch (error) {
            if (error instanceof ValidationError) {
                res.status(400).json({"message": error.message});
            } else {
                next(error);
            }
        }

    } else {
        res.status(400).json({"message": "Missing user name or password."});
    }

});

//GET /api/users/{id}/accounts -- Get a user's accounts.
router.get('/:id/accounts', async function(req, res, next) {

    let id = req.params.id;

    if (isNaN(id)) {
        res.status(400).json({"message": "A user ID is a number."});
    } else {

        try {
            const user = await User.query().where('id', id).first();
            if (user === undefined) {
                res.status(404).end();
            } else {
                const accounts = await user.$relatedQuery('accounts');
                res.json(accounts);
            }
        } catch (error) {
            next(error);
        }

    }

});

//PUT /api/users/{id}/accounts -- Set whether a user as access to an account.
router.put('/:id/accounts', async function(req, res, next) {

        if (req.params.userId && req.params.accountId && req.params.userHasAccess) {

            let accountAccess = {
                user_id = req.params.id;
                account_id = req.body.accountId;
                can_see = true;
                can_book_transaction = true;
            }

            try {
                const user = await User.query().where('id', id).first();
                if (user === undefined) {
                    res.status(404).end();
                } else {
                    await user.$relatedQuery('accounts').insert(accountAccess);
                    res.json(accounts);
                }
            } catch (error) {
                next(error);
            }

        } else {
            res.status(400).json({"message": "Missing information."});
        }

}

module.exports = router;
