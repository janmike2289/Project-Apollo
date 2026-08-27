import { useEffect, useMemo, useState } from "react";
import { getChangeTicket, listChangeTickets } from "./api";
import { TicketDetail } from "./components/TicketDetail";
import { TicketList } from "./components/TicketList";
import type { ChangeStatus, ChangeTicket, ChangeType, TicketListQuery } from "./types";

const statuses: ChangeStatus[] = [
  "Draft",
  "Submitted",
  "Approved",
  "Scheduled",
  "InProgress",
  "Completed",
  "Rejected",
  "Cancelled"
];

const changeTypes: ChangeType[] = ["Standard", "Normal", "Emergency"];

function selectedIdFromHash(): string | null {
  const match = window.location.hash.match(/^#\/tickets\/([0-9a-fA-F-]{36})$/);
  return match?.[1] ?? null;
}

export default function App() {
  const [query, setQuery] = useState<TicketListQuery>({
    title: "",
    status: "",
    changeType: "",
    requester: ""
  });
  const [tickets, setTickets] = useState<ChangeTicket[]>([]);
  const [selectedId, setSelectedId] = useState<string | null>(selectedIdFromHash);
  const [selected, setSelected] = useState<ChangeTicket | null>(null);
  const [listError, setListError] = useState<string | null>(null);
  const [detailError, setDetailError] = useState<string | null>(null);
  const [loadingList, setLoadingList] = useState(true);
  const [loadingDetail, setLoadingDetail] = useState(false);

  useEffect(() => {
    const onHashChange = () => setSelectedId(selectedIdFromHash());
    window.addEventListener("hashchange", onHashChange);
    return () => window.removeEventListener("hashchange", onHashChange);
  }, []);

  useEffect(() => {
    const controller = new AbortController();
    setLoadingList(true);
    setListError(null);

    listChangeTickets(query, controller.signal)
      .then((items) => {
        if (!controller.signal.aborted) {
          setTickets(items);
        }
      })
      .catch((error: unknown) => {
        if (controller.signal.aborted) {
          return;
        }
        setListError(error instanceof Error ? error.message : "Unable to load tickets.");
      })
      .finally(() => {
        if (!controller.signal.aborted) {
          setLoadingList(false);
        }
      });

    return () => controller.abort();
  }, [query.title, query.status, query.changeType, query.requester]);

  useEffect(() => {
    if (!selectedId) {
      setSelected(null);
      setDetailError(null);
      return;
    }

    const controller = new AbortController();
    setLoadingDetail(true);
    setDetailError(null);

    getChangeTicket(selectedId, controller.signal)
      .then((ticket) => {
        if (!controller.signal.aborted) {
          setSelected(ticket);
        }
      })
      .catch((error: unknown) => {
        if (controller.signal.aborted) {
          return;
        }
        setSelected(null);
        setDetailError(error instanceof Error ? error.message : "Unable to load ticket.");
      })
      .finally(() => {
        if (!controller.signal.aborted) {
          setLoadingDetail(false);
        }
      });

    return () => controller.abort();
  }, [selectedId]);

  const filters = useMemo(
    () => [
      { key: "title" as const, label: "Title", value: query.title ?? "", type: "text" },
      { key: "requester" as const, label: "Requester", value: query.requester ?? "", type: "text" }
    ],
    [query.title, query.requester]
  );

  function selectTicket(id: string) {
    window.location.hash = `#/tickets/${id}`;
  }

  return (
    <div className="min-h-screen">
      <header className="border-b border-slate-200 bg-white">
        <div className="mx-auto flex max-w-7xl items-center justify-between px-4 py-4">
          <div>
            <p className="text-xs font-semibold uppercase tracking-[0.2em] text-indigo-600">Apollo</p>
            <h1 className="text-lg font-semibold">Change management</h1>
          </div>
          <p className="text-sm text-slate-500">{tickets.length} tickets</p>
        </div>
      </header>

      <main className="mx-auto grid max-w-7xl grid-cols-1 gap-4 p-4 lg:grid-cols-[22rem_minmax(0,1fr)]">
        <aside className="overflow-hidden rounded-xl border border-slate-200 bg-white">
          <div className="space-y-3 border-b border-slate-200 p-4">
            {filters.map((filter) => (
              <label key={filter.key} className="block text-xs font-medium text-slate-600">
                {filter.label}
                <input
                  value={filter.value}
                  onChange={(event) => setQuery((current) => ({ ...current, [filter.key]: event.target.value }))}
                  className="mt-1 w-full rounded-lg border border-slate-300 px-3 py-2 text-sm text-slate-900 outline-none focus:border-indigo-500"
                />
              </label>
            ))}
            <label className="block text-xs font-medium text-slate-600">
              Status
              <select
                value={query.status ?? ""}
                onChange={(event) =>
                  setQuery((current) => ({ ...current, status: event.target.value as ChangeStatus | "" }))
                }
                className="mt-1 w-full rounded-lg border border-slate-300 px-3 py-2 text-sm outline-none focus:border-indigo-500"
              >
                <option value="">All statuses</option>
                {statuses.map((status) => (
                  <option key={status} value={status}>
                    {status}
                  </option>
                ))}
              </select>
            </label>
            <label className="block text-xs font-medium text-slate-600">
              Type
              <select
                value={query.changeType ?? ""}
                onChange={(event) =>
                  setQuery((current) => ({ ...current, changeType: event.target.value as ChangeType | "" }))
                }
                className="mt-1 w-full rounded-lg border border-slate-300 px-3 py-2 text-sm outline-none focus:border-indigo-500"
              >
                <option value="">All types</option>
                {changeTypes.map((type) => (
                  <option key={type} value={type}>
                    {type}
                  </option>
                ))}
              </select>
            </label>
          </div>
          {loadingList ? (
            <p className="px-4 py-8 text-center text-sm text-slate-500">Loading tickets…</p>
          ) : listError ? (
            <p className="px-4 py-8 text-center text-sm text-rose-600">{listError}</p>
          ) : (
            <TicketList tickets={tickets} selectedId={selectedId} onSelect={selectTicket} />
          )}
        </aside>

        <section>
          {!selectedId ? (
            <div className="flex h-full min-h-[24rem] items-center justify-center rounded-xl border border-dashed border-slate-300 bg-white text-sm text-slate-500">
              Select a change ticket to view its details, change log, and attachments.
            </div>
          ) : loadingDetail ? (
            <div className="flex h-full min-h-[24rem] items-center justify-center rounded-xl border border-slate-200 bg-white text-sm text-slate-500">
              Loading ticket…
            </div>
          ) : detailError ? (
            <div className="flex h-full min-h-[24rem] items-center justify-center rounded-xl border border-slate-200 bg-white text-sm text-rose-600">
              {detailError}
            </div>
          ) : selected ? (
            <TicketDetail ticket={selected} />
          ) : null}
        </section>
      </main>
    </div>
  );
}
