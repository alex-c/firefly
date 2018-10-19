const config = require('config');
const jwt = require('jsonwebtoken');

//Load User model
const User = require('../../models/User.js');

//Authentication route
async function login(req, res, next) {

    if (req.body.name && req.body.password) {

        let name = req.body.name;
        let password = req.body.password;

        try {
            const user = await User.query().where('name', name).first();
            if (user !== undefined) {
                const passwordValid = await user.verifyPassword(password);
                if (passwordValid) {

                    //Create JWT
                    const payload = {
                        user: user.name,
                        isAdmin: user.is_admin
                    };
                    const token = jwt.sign(payload, config.get('secret'));

                    //Return
                    res.json({token});

                } else {
                    //Bad password
                    res.status(401).end();
                }
            } else {
                //User does not exist
                res.status(401).end();
            }
        } catch (error) {
            next(error);
        }

    } else {
        res.status(400).json("Missing information!");
    }

}

module.exports = login;
