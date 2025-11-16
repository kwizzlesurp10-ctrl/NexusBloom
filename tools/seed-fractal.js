const { ReverseParser } = require('../shared/parser');
const fs = require('fs');
const yargs = require('yargs/yargs')(process.argv.slice(2));

const argv = yargs.option('steps', { default: 6 }).option('reverse', { type: 'boolean' }).argv;
const seed = argv._[0] || 'Design an interactive AR game for urban foraging';

const parser = new ReverseParser(seed);
(async () => {
  if (argv.reverse) {
    const recon = await parser.reconstruct();
    console.log(`Reconstructed: ${recon}`);
  } else {
    const roots = await parser.deconstruct(argv.steps);
    fs.writeFileSync('roots.json', JSON.stringify(roots, null, 2));
    console.log(roots);
  }
})();