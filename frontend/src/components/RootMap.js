import React, { useState } from 'react';
import { useQuery, useMutation } from '@tanstack/react-query';
import { Mermaid } from 'mermaid-react';
import io from 'socket.io-client';
import axios from 'axios';

const socket = io('http://localhost:3000');

export function RootMap({ traceId }) {
  const [seed, setSeed] = useState('');

  const { data: roots } = useQuery({
    queryKey: ['roots', traceId],
    queryFn: () => axios.get(`http://localhost:3000/api/prompts/${traceId}`).then(res => res.data.roots),
    enabled: !!traceId,
  });

  const deconstructMutation = useMutation({
    mutationFn: (newSeed) => axios.post('http://localhost:3000/api/parse/seed-to-roots', { seed: newSeed }),
    onSuccess: (data) => console.log('Roots:', data.roots),
  });

  const reconstructMutation = useMutation({
    mutationFn: () => axios.post('http://localhost:3000/api/synthesize/roots-to-seed', { traceId }),
    onSuccess: (data) => alert(`Reconstructed: ${data.seed}`),
  });

  const diagram = roots ? `
    graph TD
      S[Seed: ${seed}] --> R1[Step 1: ${roots[0]?.branch}]
      ${roots.map((r, i) => `R${i} --> R${i+1}[Step ${i+1}: ${r.branch}]`).slice(0, -1).join('
')}
  ` : '';

  return (
    <div>
      <input value={seed} onChange={(e) => setSeed(e.target.value)} placeholder="Plant Seed..." />
      <button onClick={() => deconstructMutation.mutate(seed)}>Fractal Down</button>
      <button onClick={() => reconstructMutation.mutate()}>Weave Up</button>
      <Mermaid chart={diagram} />
    </div>
  );
}