//Express middleware that checks for admin rights
function authorizeAdmin(req, res, next) {

    if (req.token !== undefined &&
        req.token !== null &&
        req.token.isAdmin !== undefined &&
        req.token.isAdmin === true) {
        next();
    } else {
        res.status(401).end();
    }
}

module.exports = authorizeAdmin;
