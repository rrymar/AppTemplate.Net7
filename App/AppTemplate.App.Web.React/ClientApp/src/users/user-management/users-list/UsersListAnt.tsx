import React, {useEffect, useState} from 'react';
import axios from 'axios';

import 'antd/dist/antd.css';
import {Table} from 'antd';

import {Typography} from '@material-ui/core';

import {User} from '../user';
import {SearchQuery} from 'core/searchQuery';
import {ResultsList} from 'core/resultsList';
import _ from 'lodash';
import {ColumnsType, TablePaginationConfig} from 'antd/lib/table/interface';

const columns: ColumnsType<User> = [
    {
        title: 'Id',
        dataIndex: 'id',
        sorter: true,
    },
    {
        title: 'User Name',
        dataIndex: 'username',
        sorter: true,
    },
    {
        title: 'Full Name',
        dataIndex: 'fullName',
        sorter: true,
    },
    {
        title: 'Created',
        dataIndex: 'createdOn',
        sorter: true,
    }, {
        title: 'Email',
        dataIndex: 'email',
        sorter: true,
    },
];

function UsersList() {
    const [isLoading, setIsLoadingInternal] = useState(false);
    const setIsLoading = _.debounce((state: boolean) => setIsLoadingInternal(state), 300);

    const [items, setItems] = useState<User[]>([]);

    const [sortFiled, setSortFiled] = useState('id');
    const [sortOrder, setSortOrder] = useState('ascend');

    const [page, setPage] = useState(1);
    const [pageSize, setPageSize] = useState(5);
    const [pagination, setPagination] = useState<TablePaginationConfig>({ pageSize: pageSize});

    useEffect(() => {
        setIsLoading(true);

        let query: SearchQuery = {
            pageSize: pageSize,
            pageIndex: page - 1,
            sortField: sortFiled,
            isDesc: sortOrder !== 'ascend',
            keyword: ''
        };

        axios.post<ResultsList<User>>('api/Users/Search', query)
            .then(res => {
                setItems(res.data.items);
                setPagination({...pagination,total: res.data.totalCount});
            })
            .finally(() => setIsLoading(false));
    }, [page,sortFiled, sortOrder]);

    const handleTableChange = (pagination: TablePaginationConfig,
                               filters: any, sorter: any) => {
        setSortFiled(sorter.field);
        setSortOrder(sorter.order);
        setPage(pagination.current ?? 1);
    }

    return (
        <div>
            <Typography variant="h6">
                Users
            </Typography>
            <div>
                <Table
                    columns={columns}
                    rowKey={e => e.id}
                    dataSource={items}
                    pagination={pagination}
                    loading={isLoading}
                    onChange={handleTableChange}
                />
            </div>
        </div>
    );
}

export default UsersList;
