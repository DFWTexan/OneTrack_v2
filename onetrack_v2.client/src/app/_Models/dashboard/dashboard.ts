export interface DashboardData {
    
}

export interface AuditLog {
    auditLogId: number;
    baseTableName: string | null;
    baseTableKeyValue: string | null;
    modifyDate: string;
    modifiedBy: string;
    geid: string | null;
    auditFieldName: string;
    auditAction: string;
    auditValueBefore: string | null;
    auditValueAfter: string;
  }

  export interface AuditModifyBy {
    modifiedBy: string | null;
    fullName: string;
  }