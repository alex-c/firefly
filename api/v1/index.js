const express = require('express');
const router = express.Router();

//router.get('/auth', require('./auth'));
router.use('/users', require('./users.js'));
router.use('/accounts', require('./accounts.js'));
router.use('/transactions', require('./transactions.js'));

module.exports = router;
