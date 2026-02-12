const JOB_ID_KEY = 'milledesk_jobId';

export const getJobId = () => localStorage.getItem(JOB_ID_KEY);
export const setJobId = (jobId: string) => localStorage.setItem(JOB_ID_KEY, jobId);
export const clearJobId = () => localStorage.removeItem(JOB_ID_KEY);