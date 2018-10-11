const express = require('express');
const router = express.Router();

//Load required models
const User = require('../../models/User.js');

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

//GET /api/users/accounts -- Get a user's accounts.
router.get('/:id/accounts', async function(req, res, next) {

    let id = req.params.id;

    try {
        const user = await User.query().where('id', id);
        const accounts = await user.$relatedQuery('accounts');
        res.json(accounts);
    } catch (error) {
        next(error);
    }

});

//POST /api/users -- Create a user.
router.post('/', async function(req, res, next) {

    if (req.body.name && req.body.password) {

        var user = {
            name: req.body.name,
            password: req.body.password,
            is_admin: req.body.isAdmin || false
        }

        try {
            await User.query().insert(user);
            res.status(201).end();
        } catch (error) {
            next(error);
        }

    } else {
        res.status(400).json("Missing information!");
    }

});


module.exports = router;
