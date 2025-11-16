const express = require('express');
const mongoose = require('mongoose');
const redis = require('redis');
const socketIo = require('socket.io');
const http = require('http');
const pMemoize = require('p-memoize');
const app = express();
const server = http.createServer(app);
const io = socketIo(server);
const redisClient = redis.createClient({ url: process.env.REDIS_URL || 'redis://localhost:6379' });

redisClient.connect().catch(console.error);
mongoose.connect(process.env.MONGODB_URI || 'mongodb://localhost:27017/promptEmpire', { useNewUrlParser: true });

const Prompt = require('./models/Prompt');
const parserRoutes = require('./routes/parser');

app.use(express.json());
app.use('/api', parserRoutes);

io.on('connection', (socket) => {
  socket.on('joinPrompt', (promptId) => socket.join(promptId));
  socket.on('editPrompt', (data) => io.to(data.promptId).emit('promptUpdate', data.text));
});

const memoizedAI = pMemoize(async (prompt) => {
  return new Promise(resolve => setTimeout(() => resolve(`AI Echo: ${prompt}`), 1000));
}, { cache: new Map(), maxAge: 300000 });

app.post('/preview', async (req, res) => {
  const result = await memoizedAI(req.body.prompt);
  res.json({ result });
});

server.listen(process.env.PORT || 3000, () => console.log('Nexus Spine Active on 3000'));