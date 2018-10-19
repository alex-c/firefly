const express = require('express');
const router = express.Router();

//Token parsing middleware
const parseToken = require('./middleware/parseToken.js');

//API v1
router.use('/v1', parseToken, require('./v1'));

module.exports = router;
