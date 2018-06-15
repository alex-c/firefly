const config = require('config');

//Load User model
const User = require('../../models/User.js');

//Login route
function login(req, res, next) {

    if (req.body.name && req.body.password) {

        try {
            const user = await User.query().where('name', req.body.name);
            if (user) {
                const passwordValid = await user.verifyPassword(password);
                if (passwordValid) {

                    //Create JWT
                    const payload = {
                        user: user.name,
                        isAdmin: user.isAdmin
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
