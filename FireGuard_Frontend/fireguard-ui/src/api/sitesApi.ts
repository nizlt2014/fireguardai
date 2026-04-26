import api from "./client";

export const getSites = async () => {
  const res = await api.get("/sites");
  return res.data;
};

export const createSite = async (payload: any) => {
  const res = await api.post("/sites", payload);
  return res.data;
};

export const deleteSite = async (id: number) => {
  await api.delete(`/sites/${id}`);
};