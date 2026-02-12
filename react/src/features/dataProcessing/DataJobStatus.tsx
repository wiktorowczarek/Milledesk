import React from 'react';
import type { UseDataJobReturn } from '../../hooks/types';

export const DataJobStatus: React.FC<UseDataJobReturn> = ({ status, data, error, elapsed }) => {
  if (!status) return null;

  if (status === 'Processing') {
    return (
      <div>
        <p>Processing... {elapsed}s elapsed</p>
        <div style={{ width: '100%', height: '10px', background: '#eee', borderRadius: '5px' }}>
          <div
            style={{
              width: `${Math.min((elapsed / 60) * 100, 100)}%`,
              height: '100%',
              background: '#4caf50',
              borderRadius: '5px',
              transition: 'width 1s linear',
            }}
          />
        </div>
      </div>
    );
  }

  if (status === 'Completed') {
    return (
      <div>
        <p>Completed!</p>
        <p>Data: {data}</p>
      </div>
    );
  }

  if (status === 'Failed') {
    return (
      <div>
        <p style={{ color: 'red' }}>Failed!</p>
        <p>Error: {error}</p>
      </div>
    );
  }

  return null;
};