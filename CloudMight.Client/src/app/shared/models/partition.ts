export interface Partition {
  id: string;
  name: string;
  sizeBytes: number;
  usedBytes: number;
  mainFolderId: string;
}
