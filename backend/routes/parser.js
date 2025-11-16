const express = require('express');
const router = express.Router();
const { ReverseParser } = require('../../shared/parser');
const Prompt = require('../models/Prompt');

router.post('/parse/seed-to-roots', async (req, res) => {
  const { seed, steps = 6 } = req.body;
  const parser = new ReverseParser(seed);
  const roots = await parser.deconstruct(steps);
  const prompt = new Prompt({ seed, roots });
  await prompt.save();
  res.json({ roots, traceId: prompt._id });
});

router.post('/synthesize/roots-to-seed', async (req, res) => {
  const { traceId } = req.body;
  const prompt = await Prompt.findById(traceId);
  if (!prompt) return res.status(404).json({ error: 'Lost Roots' });
  const parser = new ReverseParser(prompt.seed, prompt.roots);
  const reconstructed = await parser.reconstruct();
  res.json({ seed: reconstructed, fidelity: 0.98 });
});

module.exports = router;