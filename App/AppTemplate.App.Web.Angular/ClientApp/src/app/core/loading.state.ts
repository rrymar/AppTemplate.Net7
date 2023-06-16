export interface LoadingState {
  isLoading: boolean;
}

export interface EntityLoadingState<T> extends LoadingState {
  entity: T | null;
}

export interface ItemsLoadingState<T> extends LoadingState {
  items: T[];
}
