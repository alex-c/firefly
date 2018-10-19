//Express middleware that check for successful token parsing
function authorizeUser(req, res, next) {

    if (req.token !== undefined && req.token !== null) {
        next();
    } else {
        res.status(401).end();
    }
}

module.exports = authorizeUser;
