import React, {useEffect, useState} from 'react';
import {Link as RouterLink} from 'react-router-dom';
import axios from 'axios';
import _ from 'lodash';

import {
    Columns,
    DataGrid,
    PageChangeParams, RowSelectedParams,
    SortDirection,
    SortModelParams,
} from '@material-ui/data-grid';

import {useHistory} from 'react-router-dom';

import {Button, Typography} from '@material-ui/core';

import {User} from '../user';
import {SearchQuery} from 'core/searchQuery';
import {ResultsList} from 'core/resultsList';
import {parseIsoDate} from 'core/formatting';


const columns: Columns = [
    {field: 'id', headerName: 'Id', flex: 2},
    {field: 'username', headerName: 'User Name', flex: 5},
    {field: 'fullName', headerName: 'Full name', flex: 10},
    {
        field: 'createdOn', headerName: 'Created On', flex: 10, type: 'dateTime',
        valueGetter: (p) => parseIsoDate(p.value)
    },
    {field: 'email', headerName: 'Email', flex: 10},
];

function UsersList() {
    const [isLoading, setIsLoadingInternal] = useState(false);
    const setIsLoading = _.debounce((state: boolean) => setIsLoadingInternal(state), 300);

    const [items, setItems] = useState<User[]>([]);

    const [sortField, setSortField] = useState('id');
    const [sortOrder, setSortOrder] = useState<SortDirection>('asc');

    const [page, setPage] = useState(1);
    const [pageSize] = useState(5);

    const [totalCount, setTotalCount] = useState<number>(0);

    useEffect(() => {
        setIsLoading(true);

        let query: SearchQuery = {
            pageSize: pageSize,
            pageIndex: page - 1,
            sortField: sortField,
            isDesc: sortOrder === 'desc',
            keyword: ''
        };

        axios.post<ResultsList<User>>('/api/Users/Search', query)
            .then(res => {
                setTotalCount(res.data.totalCount);
                setItems(res.data.items);
            })
            .finally(() => setIsLoading(false));
    }, [page, pageSize, sortField, sortOrder]);

    const handlePageChange = (params: PageChangeParams) => {
        setPage(params.page);
    };

    const handleSortModelChange = (params: SortModelParams) => {
        setSortField(params.sortModel[0]?.field);
        setSortOrder(params.sortModel[0]?.sort);
    };

    let history = useHistory();

    const handleRowSelected = (params: RowSelectedParams) => {
        history.push(`users/${params.data.id}`);
    };

    return (
        <div>
            <Typography variant="h6">
                Users
                <Button color="inherit" component={RouterLink} to="users/new">
                    Create New
                </Button>
            </Typography>


            <div>
                <DataGrid autoHeight={true}
                          key={'id'}
                          rows={items}
                          columns={columns}
                          loading={isLoading}
                          pageSize={pageSize}
                          paginationMode={'server'}
                          rowCount={totalCount}
                          onPageChange={handlePageChange}
                          sortingMode={'server'}
                          onSortModelChange={handleSortModelChange}
                          onRowSelected={handleRowSelected}
                />
            </div>
        </div>
    );
}

export default UsersList;
