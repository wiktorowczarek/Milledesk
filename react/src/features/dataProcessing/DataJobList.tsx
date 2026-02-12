import React from 'react';
import type { Job } from '../../hooks/useDataJobs';

type Props = { jobs: Job[] };

export const DataJobList: React.FC<Props> = ({ jobs }) => {
  return (
    <div className="job-history">
      <h2>Job History</h2>
      {jobs.map((job) => (
        <div key={job.jobId} className="job-card">
          <p>
            <strong>Job ID:</strong> {job.jobId}
          </p>
          <p>
            <strong>Status:</strong>{' '}
            <span className={`job-status ${job.status === 'Failed' ? 'failed' : ''}`}>{job.status}</span>
          </p>
          {job.status === 'Processing' && <p>Elapsed: {job.elapsed}s</p>}
          {job.status === 'Completed' && <p>Data: {job.data}</p>}
          {job.status === 'Failed' && <p>Error: {job.error}</p>}
          {job.status === 'Processing' && (
            <div className="progress-container">
              <div
                className="progress-bar"
                style={{ width: `${Math.min((job.elapsed / 60) * 100, 100)}%` }}
              />
            </div>
          )}
        </div>
      ))}
    </div>
  );
};
