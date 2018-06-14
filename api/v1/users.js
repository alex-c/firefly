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

//POST /api/users -- Create a user.
router.post('/', async function(req, res, next) {

    if (req.body.name && req.body.password) {

        try {
            await User.query().insert({name: req.body.name, password: req.body.password});
            res.status(201).end();
        } catch (error) {
            next(error);
        }

    } else {
        res.status(400).json("Missing information!");
    }

});


module.exports = router;
