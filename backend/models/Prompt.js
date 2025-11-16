const mongoose = require('mongoose');
const PromptSchema = new mongoose.Schema({
  seed: { type: String, required: true },
  roots: [{ step: Number, branch: String, prompt: String, type: String, codeShard: String }],
  metadata: { owner: String, price: Number, analytics: { deconstructions: Number } }
});
module.exports = mongoose.model('Prompt', PromptSchema);