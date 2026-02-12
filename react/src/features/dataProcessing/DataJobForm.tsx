import React from 'react';
import type { UseDataJobReturn } from '../../hooks/types';

export const DataJobForm: React.FC<UseDataJobReturn> = ({ start, jobId, status }) => {
  return (
    <div style={{ marginBottom: '1rem' }}>
      <button onClick={start} style={{ padding: '0.5rem 1rem' }}>
        Start Data Job
      </button>

      {jobId && (
        <p style={{ marginTop: '0.5rem' }}>
          <strong>Job ID:</strong> {jobId} &nbsp; | &nbsp;
          <strong>Status:</strong> {status}
        </p>
      )}
    </div>
  );
};