import { useState, useEffect, useCallback } from 'react';
import { startJob as apiStartJob, getJobStatus as apiGetJobStatus } from '../api/dataApi';

export type Job = {
  jobId: string;
  status: 'Processing' | 'Completed' | 'Failed';
  elapsed: number;
  data?: string;
  error?: string;
};

export const useDataJobs = () => {
  const [jobs, setJobs] = useState<Job[]>([]);

  const startJob = useCallback(async () => {
    const jobId = await apiStartJob();
    const newJob: Job = { jobId, status: 'Processing', elapsed: 0 };
    setJobs((prev) => [newJob, ...prev]);
  }, []);

  useEffect(() => {
  const interval = setInterval(() => {
    setJobs((prevJobs) =>
      prevJobs.map((job) => {
        if (job.status === 'Processing') return { ...job, elapsed: job.elapsed + 1 };
        return job;
      })
    );

    jobs.forEach(async (job) => {
      if (job.status !== 'Processing') return;

      try {
        const res = await apiGetJobStatus(job.jobId);
        if (res.status === 'Completed') {
          setJobs((prev) =>
            prev.map((j) =>
              j.jobId === job.jobId ? { ...j, status: 'Completed', data: res.data } : j
            )
          );
        } else if (res.status === 'Failed') {
          setJobs((prev) =>
            prev.map((j) =>
              j.jobId === job.jobId ? { ...j, status: 'Failed', error: res.error || 'Unknown error' } : j
            )
          );
        }
      } catch (err: any) { // TODO: Create proper error handling - dont use any (couldnt manage to do that due to limited time)
        console.error(err);

        setJobs((prev) =>
          prev.map((j) =>
            j.jobId === job.jobId
              ? { ...j, status: 'Failed', error: err.response?.data?.error || err.message || 'Server error' }
              : j
          )
        );
      }
    });
  }, 1000);

  return () => clearInterval(interval);
}, [jobs]);

  return { jobs, startJob };
};
