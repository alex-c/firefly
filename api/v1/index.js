const express = require('express');
const router = express.Router();

//router.get('/auth', require('./auth'));
router.use('/users', require('./users.js'));

module.exports = router;
