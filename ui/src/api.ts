import type { ChangeTicket, TicketListQuery } from "./types";

function toQuery(params: TicketListQuery): string {
  const search = new URLSearchParams();
  if (params.title) search.set("title", params.title);
  if (params.status) search.set("status", params.status);
  if (params.changeType) search.set("changeType", params.changeType);
  if (params.requester) search.set("requester", params.requester);
  const query = search.toString();
  return query ? `?${query}` : "";
}

async function readJson<T>(response: Response): Promise<T> {
  if (!response.ok) {
    throw new Error(`API request failed (${response.status})`);
  }
  return (await response.json()) as T;
}

export async function listChangeTickets(
  query: TicketListQuery,
  signal?: AbortSignal
): Promise<ChangeTicket[]> {
  return readJson<ChangeTicket[]>(await fetch(`/change-tickets${toQuery(query)}`, { signal }));
}

export async function getChangeTicket(id: string, signal?: AbortSignal): Promise<ChangeTicket> {
  return readJson<ChangeTicket>(await fetch(`/change-tickets/${id}`, { signal }));
}
