const config = require('config');
const { Model } = require('objection');
const Knex = require('knex');

//Initialize knex
const knex = Knex({
    client: 'pg',
    connection: config.get('db.connection'),
    pool: config.get('db.pool')
});

//Set up objection
Model.knex(knex);
