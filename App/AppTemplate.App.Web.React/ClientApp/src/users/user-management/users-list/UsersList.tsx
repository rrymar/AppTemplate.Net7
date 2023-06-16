import React, {useEffect, useState} from 'react';
import axios from 'axios';

import {DataTable} from 'primereact/datatable';
import {Column} from 'primereact/column';

import 'primeicons/primeicons.css';
import 'primereact/resources/themes/saga-blue/theme.css';
import 'primereact/resources/primereact.css';
import 'primeflex/primeflex.css';

import {Typography} from '@material-ui/core';

import {User} from '../user';
import {SearchQuery} from 'core/searchQuery';
import {ResultsList} from 'core/resultsList';
import _ from 'lodash';

function UsersList() {
    const [isLoading, setIsLoadingInternal] = useState(false);
    const setIsLoading = _.debounce((state: boolean) => setIsLoadingInternal(state), 300);

    const [items, setItems] = useState<User[]>([]);

    const [sortFiled, setSortFiled] = useState('id');
    const [sortOrder, setSortOrder] = useState(1);

    const [pageIndex, setPageIndex] = useState(0);
    const [pageSize] = useState(5);

    const [totalCount, setTotalCount] = useState<number>(0);

    useEffect(() => {
        setIsLoading(true);

        let query: SearchQuery = {
            pageSize: pageSize,
            pageIndex: pageIndex,
            sortField: sortFiled,
            isDesc: sortOrder != 1,
            keyword: ''
        };

        axios.post<ResultsList<User>>('api/Users/Search', query)
            .then(res => {
                setItems(res.data.items);
                setTotalCount(res.data.totalCount);
            })
            .finally(() => setIsLoading(false));
    }, [pageIndex, pageSize, sortFiled, sortOrder]);

    const onPageIndexChanged = (event: any) => {
        setPageIndex(event.page);
    };

    const onSortChanged = (event: any) => {
        setPageIndex(0);
        setSortFiled(event.sortField);
        setSortOrder(event.sortOrder);
    };
    const firstRowIndex = pageIndex * pageSize + 1;
    return (
        <div>
            <Typography variant="h6">
                Users
            </Typography>
            <div className="card">
                <DataTable value={items}
                           paginator rows={pageSize}
                           totalRecords={totalCount}
                           lazy first={firstRowIndex}
                           onPage={onPageIndexChanged}
                           sortField={sortFiled}
                           sortOrder={sortOrder}
                           onSort={onSortChanged}
                           loading={isLoading}>
                    <Column field="id" header="Id" sortable></Column>
                    <Column field="username" header="User Name" sortable></Column>
                    <Column field="fullName" header="Full Name" sortable></Column>
                    <Column field="createdOn" header="Created" sortable></Column>
                    <Column field="email" header="Email" sortable></Column>
                </DataTable>
            </div>
        </div>
    );
}

export default UsersList;
