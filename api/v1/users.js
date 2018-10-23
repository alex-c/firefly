const express = require('express');
const router = express.Router();

//Load required models and error objects
const User = require('../../models/User.js');
const { ValidationError } = require('objection');

//Authorization middleware
let authorizeAdmin = require('./middleware/authorizeAdmin.js');

//GET /users -- Get a list of users.
router.get('/', async function(req, res, next) {

    try {
        const users = await User.query();
        res.json(users);
    } catch (error) {
        next(error);
    }

});

//GET /users/{id} -- Get a specific user.
router.get('/:id', async function(req, res, next) {

    let id = req.params.id;

    if (isNaN(id)) {
        res.status(400).json({"message": "A user ID is a number."});
    } else {

        try {
            const user = await User.query().where('id', id).first();
            if (user !== undefined) {
                res.json(user);
            } else {
                res.status(404).end();
            }
        } catch (error) {
            next(error);
        }

    }

});

//POST /users -- Create a user.
router.post('/', authorizeAdmin, async function(req, res, next) {

    if (req.body.name && req.body.password) {

        let user = {
            name: req.body.name,
            password: req.body.password,
            is_admin: req.body.isAdmin || false
        }

        try {
            user = await User.query().insert(user);
            res.status(201).json(user);
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

//GET /users/{id}/accounts -- Get a user's accounts.
router.get('/:id/accounts', async function(req, res, next) {

    let id = req.params.id;

    if (isNaN(id)) {
        res.status(400).json({"message": "A user ID is a number."});
    } else {

        try {
            const user = await User.query().where('id', id).first();
            if (user !== undefined) {
                const accounts = await user.$relatedQuery('accounts');
                res.json(accounts);
            } else {
                res.status(404).end();
            }
        } catch (error) {
            next(error);
        }

    }

});

//PUT /users/{id}/accounts -- Set whether a user as access to an account.
router.put('/:id/accounts', authorizeAdmin, async function(req, res, next) {

    if (req.params.id && req.body.accountId && req.body.canSee !== undefined && req.body.canBookTransaction !== undefined) {

        let userId = req.params.id;
        let accountAccess = {
            id: req.body.accountId,
            can_see: req.body.canSee,
            can_book_transaction: req.body.canBookTransaction
        };

        if (isNaN(userId) || isNaN(accountAccess.id)) {
            res.status(400).json({"message": "User and account IDs should be numbers."});
        } else {

            try {
                const user = await User.query().where('id', userId).first();
                if (user !== undefined) {
                    await user.$relatedQuery('accounts').unrelate().where('account_id', accountAccess.id);
                    await user.$relatedQuery('accounts').relate(accountAccess);
                    res.status(200).end();
                } else {
                    res.status(404).end();
                }
            } catch (error) {
                next(error);
            }

        }

    } else {
        res.status(400).json({"message": "Missing information."});
    }
});

//DELETE /users/{id} -- Delete a specific user.
router.delete('/:id', authorizeAdmin, async function(req, res, next) {

    let id = req.params.id;

    if (isNaN(id)) {
        res.status(400).json({"message": "A user ID is a number."});
    } else {

        try {
            //Remove access rights to accounts
            const user = await User.query().where('id', id).first();
            if (user !== undefined) {
                await user.$relatedQuery('accounts').unrelate();
            }
            //Delete user
            await User.query().delete().where('id', id);
            res.end();
        } catch (error) {
            next(error);
        }

    }

});

module.exports = router;
