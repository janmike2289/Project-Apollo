export type ChangeType = "Standard" | "Normal" | "Emergency";
export type ChangeStatus =
  | "Draft"
  | "Submitted"
  | "Approved"
  | "Scheduled"
  | "InProgress"
  | "Completed"
  | "Rejected"
  | "Cancelled";
export type ChangePriority = "Low" | "Medium" | "High" | "Critical";
export type AttachmentKind = "Email" | "Screenshot";

export interface ChangeLogComment {
  id: string;
  body: string;
  author: string;
  createdAt: string;
}

export interface Attachment {
  id: string;
  kind: AttachmentKind;
  fileName: string;
  contentType: string;
  storageKey: string;
  emailFrom: string | null;
  emailSubject: string | null;
  createdAt: string;
}

export interface ChangeTicket {
  id: string;
  title: string;
  description: string;
  changeType: ChangeType;
  status: ChangeStatus;
  priority: ChangePriority;
  requester: string;
  assignedTo: string | null;
  implementationPlan: string | null;
  rollbackPlan: string | null;
  scheduledStart: string | null;
  scheduledEnd: string | null;
  createdAt: string;
  updatedAt: string;
  changeLog: ChangeLogComment[];
  attachments: Attachment[];
}

export interface TicketListQuery {
  title?: string;
  status?: ChangeStatus | "";
  changeType?: ChangeType | "";
  requester?: string;
}
